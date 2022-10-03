export type ApplicationInfo = {
  title: string;
  preview: any;
  website?: string; // optional
  description: string;
  category?: 'example-app'; // optional
};

export const apps: ApplicationInfo[] = [
  {
    title: 'State Machine',
    preview: 'img/example-state-machine.webp',
    description:
      'A state machine application example demonstrating the usage of StateNode and Connection.OffsetMode',
    website:
      'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine',
    category: 'example-app',
  },
  {
    title: 'Calculator',
    preview: 'img/example-calculator.webp',
    description:
      'A calculator application example showcasing customized nodes and nested graph editors',
    website:
      'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator',
    category: 'example-app',
  },
  {
    title: 'Playground',
    preview: 'img/example-playground.webp',
    description:
      'A playground application where you can test all the available settings',
    website:
      'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground',
    category: 'example-app',
  },
];
