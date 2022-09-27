import { GithubButton } from './components';

function App() {
	return (
		<main>
			<div className="max-w-screen-lg mx-auto">
				<section className="h-screen flex flex-col items-center justify-center gap-8">
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
					</div>
				</section>
			</div>
		</main>
	);
}

export default App;
