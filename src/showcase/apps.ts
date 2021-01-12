// READ ME!
// 'preview' image must be stored inside 'img' folder
// at least one of 'website' and 'source' must be specified

export type ApplicationInfo = {
	title: string;
	preview: any;
	website?: string; // optional
	source?: string; // optional
	description?: string; // optional
	category?: 'example-app'; // optional
};

const apps: ApplicationInfo[] = [
	// Example apps:
	{
		title: 'State Machine',
		preview: require('./img/example-state-machine.png'),
		source:
			'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine',
		category: 'example-app',
	},
	{
		title: 'Calculator',
		preview: require('./img/example-calculator.png'),
		source:
			'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator',
		category: 'example-app',
	},
	{
		title: 'Playground',
		preview: require('./img/example-playground.png'),
		source:
			'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground',
		category: 'example-app',
	},
	// Other apps:
];

export default apps;
