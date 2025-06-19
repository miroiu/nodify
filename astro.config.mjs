import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import starlightThemeObsidian from 'starlight-theme-obsidian';

import tailwindcss from '@tailwindcss/vite';

// https://astro.build/config
export default defineConfig({
  output: 'static',
  site: 'https://miroiu.github.io',
  base: '/nodify',
  build: {
    format: 'file',
  },
  trailingSlash: 'never',
  integrations: [
    starlight({
      title: 'Nodify',
      logo: {
        src: './src/assets/logo.svg',
      },
      credits: true,
      pagination: false,
      plugins: [starlightThemeObsidian({ graph: false, backlinks: false })],
      editLink: {
        baseUrl: 'https://github.com/miroiu/nodify/edit/master/docs/',
      },
      components: {
        EditLink: './src/components/EditLink.astro',
        SiteTitle: './src/components/SiteTitle.astro',
      },
      social: [
        {
          icon: 'github',
          label: 'GitHub',
          href: 'https://github.com/miroiu/nodify',
        },
      ],
      customCss: ['./src/styles/starlight.css'],
    }),
  ],
  vite: {
    plugins: [tailwindcss()],
  },
});
