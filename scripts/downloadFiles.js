import Path from 'path';
import Fs from 'fs/promises';

/**
 * Fetches markdown files from a specified folder in the GitHub repository.
 *
 * @param {string?} folder - The folder path relative to the `docs` directory in the repository.
 * @returns {Promise<Array<{ name: string, download_url: string }>>}
 */
const getMarkdownFiles = async folder => {
  const res = await fetch(
    'https://api.github.com/repos/miroiu/nodify/contents/docs' + (folder || ''),
    { headers: { Accept: 'application/vnd.github.object+json' } }
  );
  const directory = await res.json();
  const markdownFiles = directory.entries
    .filter(file => file.type == 'file' && file.name.endsWith('.md'))
    .map(file => ({
      name: file.name,
      download_url: file.download_url,
    }));

  return markdownFiles;
};

/**
 * Downloads files from the specified github folder excluding the specified excluded files.
 *
 * @param {string?} folder - The folder path relative to the `docs` directory in the repository.
 * @param {string[]?} excludedFiles - The files to exclude.
 */
async function* downloadFiles(excludedFiles = [], folder) {
  for (const file of (await getMarkdownFiles(folder)).filter(
    file => !excludedFiles.includes(file.name)
  )) {
    const filename = Path.parse(file.name).name;
    const res = await fetch(file.download_url);
    const raw = await res.text();

    yield {
      name: filename,
      data: raw,
    };
  }
}

/**
 * Writes the downloaded files to the content/docs/wiki folder.
 *
 * @param {string?} folder - The folder path relative to the `docs` directory in the repository.
 * @param {string[]?} excludedFiles - The files to exclude.
 */
const generateDocs = async (excludedFiles = [], folder) => {
  for await (const file of downloadFiles(excludedFiles, folder)) {
    const outputDir = Path.resolve(
      import.meta.dirname,
      Path.join('../src/content/docs/wiki', folder || '')
    );

    const title = generatePageTitle(file.name);

    const filePath = Path.join(outputDir, `${file.name}.md`);
    const fileData = `---
title: ${title}
sidebar:
  label: ${title}
---

${file.data}
`;

    await Fs.writeFile(filePath, fileData, 'utf8');
  }
};

/**
 * @param {string} title
 */
function generatePageTitle(title) {
  return title
    .replace('Nodify_', '')
    .replaceAll('_TElement_', '<TElement>')
    .split('_')
    .filter(Boolean)
    .join(' / ')
    .replaceAll('-', ' ');
}

async function main() {
  await generateDocs(['_Sidebar.md', 'Documentation.md']);
  await generateDocs([], '/api');
}

await main();
