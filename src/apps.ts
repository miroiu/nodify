import stateMachinePreview from '../public/img/example-state-machine.webp';
import calculatorPreview from '../public/img/example-calculator.webp';
import playgroundPreview from '../public/img/example-playground.webp';

export type ApplicationInfo = {
  title: string;
  preview: ImageMetadata;
  website?: string; // optional
  description: string;
  category?: 'example-app'; // optional
};

export const apps: ApplicationInfo[] = [
  {
    title: 'State Machine',
    preview: stateMachinePreview,
    description:
      'A state machine application example demonstrating the usage of StateNode and Connection.OffsetMode',
    website:
      'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine',
    category: 'example-app',
  },
  {
    title: 'Calculator',
    preview: calculatorPreview,
    description:
      'A calculator application example showcasing customized nodes and nested graph editors',
    website:
      'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator',
    category: 'example-app',
  },
  {
    title: 'Playground',
    preview: playgroundPreview,
    description:
      'A playground application where you can test all the available settings',
    website:
      'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground',
    category: 'example-app',
  },
];
