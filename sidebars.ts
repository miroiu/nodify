module.exports = {
	docs: [
		'introduction',
		{
			type: 'category',
			label: 'Getting Started',
			items: [
				'getting-started/installation',
				'getting-started/hierarchy',
				'getting-started/usage',
			],
			collapsed: false,
		},
		{
			type: 'category',
			label: 'Components',
			items: [
				'components/editor',
				'components/nodes',
				'components/connectors',
				'components/connections',
			],
			collapsed: true,
		},
		'faq',
	],
};
