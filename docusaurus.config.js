module.exports = {
	title: 'Nodify',
	tagline: 'Build high-performance node-based editors quickly',
	url: 'https://miroiu.github.io',
	baseUrl: '/nodify/',
	onBrokenLinks: 'throw',
	onBrokenMarkdownLinks: 'warn',
	favicon: 'img/favicon.ico',
	organizationName: 'miroiu', // Usually your GitHub org/user name.
	projectName: 'nodify', // Usually your repo name.
	themeConfig: {
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
					label: 'Docs',
					to: 'docs/',
					activeBasePath: 'docs',
					position: 'left',
				},
				{
					label: 'Showcase',
					to: 'showcase/',
					activeBasePath: 'showcase',
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
			apiKey: '9c157424075609bedcc0a0b8e6e6b7bb',
			indexName: 'prod_nodify_docs',
			appId: 'FQ9D96BE5H',
			placeholder: 'Search docs',
			disableUserPersonalization: true,
			// Optional: Algolia search parameters (https://autocomplete-experimental.netlify.app/docs/docsearchmodal/#searchparameters)
			searchParameters: {},
			// Optional: see doc section bellow
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
