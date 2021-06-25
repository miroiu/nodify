module.exports = {
	title: 'Nodify',
	tagline: 'Build high-performance node-based editors quickly',
	url: 'https://miroiu.github.io/nodify',
	baseUrl: '/nodify/',
	onBrokenLinks: 'throw',
	onBrokenMarkdownLinks: 'warn',
	favicon: 'img/favicon.ico',
	organizationName: 'miroiu', // Usually your GitHub org/user name.
	projectName: 'nodify', // Usually your repo name.
	themeConfig: {
		prism: {
			additionalLanguages: ['csharp'],
			theme: require('prism-react-renderer/themes/shadesOfPurple'),
		},
		colorMode: {
			respectPrefersColorScheme: true,
		},
		image: 'img/nodify_soc.png',
		announcementBar: {
			id: 'support_us',
			content:
				'⭐ If you like Nodify, give it a star on <a target="_blank" rel="noopener noreferrer" href="https://github.com/miroiu/nodify">Github</a>! ⭐',
			backgroundColor: '#fafbfc', // Defaults to `#fff`.
			textColor: '#091E42', // Defaults to `#000`.
			isCloseable: true, // Defaults to `true`.
		},
		navbar: {
			title: 'Nodify',
			logo: {
				alt: 'Nodify Logo',
				src: 'img/logo.svg',
			},
			items: [
				{
					type: 'doc',
					docId: 'introduction',
					label: 'Docs',
					position: 'left',
				},
				{
					type: 'doc',
					docId: 'api/Alignment',
					label: 'API',
					position: 'left',
				},
				{
					label: 'Nuget',
					href: 'https://www.nuget.org/packages/Nodify',
					position: 'right',
				},
				{
					label: 'GitHub',
					href: 'https://www.github.com/miroiu/nodify',
					position: 'right',
				},
			],
		},
		footer: {
			style: 'dark',
		},
		algolia: {
			apiKey: '42b4e84e4e495862fb4446497c540514',
			indexName: 'miroiu',
			contextualSearch: true,
		},
	},
	presets: [
		[
			'@docusaurus/preset-classic',
			{
				docs: {
					sidebarPath: require.resolve('./sidebars.ts'),
					editUrl: 'https://github.com/miroiu/nodify/edit/docs/',
				},
				theme: {
					customCss: require.resolve('./src/theme/custom.css'),
				},
			},
		],
	],
	plugins: [
		[
			'@docusaurus/plugin-pwa',
			{
				offlineModeActivationStrategies: ['always'],
				pwaHead: [
					{
						tagName: 'link',
						rel: 'manifest',
						href: 'manifest.json', // your PWA manifest
					},
					{
						tagName: 'meta',
						name: 'theme-color',
						content: '#f4f1fa',
					},
				],
			},
		],
		[
			'@docusaurus/plugin-ideal-image',
			{
				quality: 70,
				max: 1030, // max resized image's size.
				min: 640, // min resized image's size. if original is lower, use that size.
				steps: 2, // the max number of images generated between min and max (inclusive)
			},
		],
	],
};
