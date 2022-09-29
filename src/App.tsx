import { GithubButton } from './components';

export type ApplicationInfo = {
	title: string;
	preview: any;
	website?: string; // optional
	source?: string; // optional
	description?: string; // optional
	category?: 'example-app'; // optional
};

const apps: ApplicationInfo[] = [
	{
		title: 'State Machine',
		preview: 'img/example-state-machine.png',
		description: 'Lorem ipsum isidor asdmer asdfasd mamer adxfae msafm',
		website: 'https://asd.com',
		source: 'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine',
		category: 'example-app',
	},
	{
		title: 'Calculator',
		preview: 'img/example-calculator.png',
		source: 'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator',
		category: 'example-app',
	},
	{
		title: 'Playground',
		preview: 'img/example-playground.png',
		source: 'https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground',
		category: 'example-app',
	},
];

const features = [
	{
		title: 'Built for MVMM',
		description: 'Designed from the ground up to work with MVVM',
	},
	{
		title: 'Built for MVMM',
		description: 'Designed from the ground up to work with MVVM',
	},
	{
		title: 'Built for MVMM',
		description: 'Designed from the ground up to work with MVVM',
	},
	{
		title: 'Built for MVMM',
		description: 'Designed from the ground up to work with MVVM',
	},
	{
		title: 'Built for MVMM',
		description: 'Designed from the ground up to work with MVVM',
	},
	{
		title: 'Built for MVMM',
		description: 'Designed from the ground up to work with MVVM',
	},
];

function App() {
	return (
		<main>
			<div className="max-w-screen-lg mx-auto">
				<section className="h-screen flex flex-col items-center justify-center gap-8 relative">
					<div className="flex gap-20">
						<div className="flex flex-col gap-16">
							<h1 className="text-6xl tracking-tight font-extrabold">
								Build{' '}
								<span className="text-primary">node-based</span>{' '}
								editors quickly, focus on your business{' '}
								<span className="text-primary">logic</span>.
							</h1>

							<div className="flex gap-6 items-center">
								<a
									className="bg-primary hover:bg-primary-darker transition-colors duration-200 ease-in-out py-4 px-6 font-bold text-3xl uppercase rounded-md"
									href="https://github.com/miroiu/nodify/wiki/Getting-Started"
								>
									Get started
								</a>
								<GithubButton owner="miroiu" repo="nodify" />
							</div>
						</div>

						<img
							src="logo.svg"
							className="w-64 h-64 hover:scale-105 transition-all duration-200 ease-in-out"
						/>
						<div className="absolute bottom-0 w-full flex flex-col justify-center items-center py-6">
							<a
								href="#showcase"
								className="flex flex-col items-center justify-center group"
							>
								<p className="text-xl mb-2 font-semibold opacity-75 group-hover:opacity-100 transition-opacity duration-200 ease-in-out">
									Take a look
								</p>
								<img
									src="img/carret.svg"
									alt="scroll"
									className="h-12 w-12 animate-bounce"
								/>
							</a>
						</div>
					</div>
				</section>
				<section
					id="showcase"
					className="scroll-m-4 mt-28 grid grid-cols-3 gap-8"
				>
					<div className="flex flex-col gap-8 col-span-2">
						{apps.map(app => (
							<div className="grid grid-cols-2 shadow-2xl">
								<img
									src={app.preview}
									alt={app.title}
									className="w-full h-48 rounded-tl-md rounded-bl-md"
								/>
								<div className="bg-card backdrop-blur flex flex-col p-4 rounded-tr-md rounded-br-md">
									<h3 className="text-primary text-2xl font-extrabold">
										{app.title}
									</h3>
									{app.description && (
										<p className="">{app.description}</p>
									)}
									<div className="flex gap-3 mt-auto">
										{app.source && (
											<a className="flex items-center justify-center grow border p-1 font-bold rounded-md border-primary hover:bg-primary transition-colors duration-200 ease-in-out">
												Source
											</a>
										)}
										{app.website && (
											<a className="flex items-center justify-center grow border p-1 font-bold rounded-md border-secondary hover:bg-secondary transition-colors duration-200 ease-in-out">
												Visit
											</a>
										)}
									</div>
								</div>
							</div>
						))}
					</div>
					<div className="h-full w-full flex flex-col rounded-md gap-4 bg-card">
						{features.map(feature => (
							<div className="p-4">
								<h4 className="text-2xl font-semibold">
									{feature.title}
								</h4>
								<p>{feature.description}</p>
							</div>
						))}
					</div>
				</section>
			</div>
			<footer className="flex flex-col gap-4 justify-center items-center py-12 mt-40 bg-card">
				<div className="flex gap-2 font-semibold uppercase">
					<a>Docs</a>·<a>Github</a>·<a>Nuget</a>
				</div>
				<p className="text-primary">Copyright © 2022 Emanuel Miroiu</p>
				<h6 className="text-sm">
					Designed with ❤️ by{' '}
					<a
						className="text-secondary"
						href="https://github.com/MiroiuGabriel"
						target="_blank"
					>
						MiroiuGabriel
					</a>
				</h6>
			</footer>
		</main>
	);
}

export default App;
