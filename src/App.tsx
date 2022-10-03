import { apps } from './apps';
import { GithubButton } from './components';

const features = [
  {
    title: 'Built for MVMM',
    description: 'Designed from the ground up to work with MVVM',
  },
  {
    title: 'High performance',
    description: 'Optimized for interaction with hundreds of nodes at once',
  },
  {
    title: 'Customizable',
    description:
      'Use the built-in themes or create your own with the power of XAML',
  },
  {
    title: 'Featureful',
    description: 'Fully featured navigation, zoom and area selection system',
  },
];

function App() {
  return (
    <div>
      <div className="max-w-screen-lg mx-auto">
        <section className="h-screen flex flex-col items-center justify-center gap-8 relative">
          <div className="flex gap-20">
            <div className="flex flex-col gap-16">
              <h1 className="text-6xl tracking-tight font-extrabold">
                Build <span className="text-primary">node-based</span> editors
                quickly, focus on your business{' '}
                <span className="text-primary">logic</span>.
              </h1>

              <div className="flex gap-6 items-center">
                <a
                  className="bg-primary hover:bg-primary-darker transition-colors duration-200 ease-in-out py-4 px-6 font-bold text-3xl uppercase rounded-lg shadow-sm"
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
          className="scroll-m-4 pt-28 flex flex-col gap-16"
        >
          <main className="grid grid-cols-4 gap-6">
            {features.map(feature => (
              <div className="relative px-2 py-4 rounded-lg bg-gradient-to-r from-primary-darker to-secondary shadow-lg flex justify-center items-center group">
                <h4 className="text-xl font-semibold uppercase group-hover:opacity-0">
                  {feature.title}
                </h4>
                <div className="absolute inset-0 flex items-center text-center">
                  <p className="text-sm font-semibold opacity-0 group-hover:opacity-100">
                    {feature.description}
                  </p>
                </div>
              </div>
            ))}
          </main>

          <div className="grid grid-cols-2 gap-8">
            {apps.map(app => (
              <div className="rounded-lg bg-gradient-to-t from-secondary via-secondary to-primary p-1 shadow-2xl overflow-hidden flex flex-col">
                <div className="relative group">
                  <img
                    src={app.preview}
                    alt={app.title}
                    className="w-full h-64 object-center object-cover rounded-t-md"
                  />
                  {app.website && (
                    <div>
                      <span className="flex h-4 w-4 absolute top-2 right-2 group-hover:opacity-0 transition-opacity duration-200">
                        <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-primary opacity-75"></span>
                        <span className="relative inline-flex rounded-full h-4 w-4 bg-primary"></span>
                      </span>
                      <a
                        href={app.website}
                        className="absolute top-0 right-0 w-full h-full [clip-path:circle(0%_at_100%_0)] group-hover:[clip-path:circle(150%_at_100%_0)] motion-safe:transition-all duration-200 bg-secondary opacity-90 flex justify-center items-center"
                      >
                        <img
                          src="img/external-link.svg"
                          className="w-12 h-12"
                        />
                      </a>
                    </div>
                  )}
                </div>
                <div className="bg-card backdrop-blur flex flex-col p-4 rounded-tr-md rounded-br-md grow">
                  <h3 className="text-xl font-extrabold mb-2">{app.title}</h3>
                  {app.description && <p>{app.description}</p>}
                </div>
              </div>
            ))}
            <div className="rounded-lg bg-gradient-to-t from-secondary via-secondary to-primary p-1 shadow-2xl overflow-hidden flex flex-col">
              <a
                href="https://github.com/miroiu/nodify/edit/docs/src/apps.ts"
                className="h-full bg-secondary opacity-90 flex justify-center items-center group"
              >
                <img
                  src="img/plus.svg"
                  className="w-16 h-16 group-hover:w-20 group-hover:h-20 transition-all"
                />
              </a>
            </div>
          </div>
        </section>
      </div>
      <footer className="flex flex-col gap-4 justify-center items-center py-12 mt-32 bg-card">
        <div className="flex gap-2 font-semibold uppercase">
          <a
            href="https://github.com/miroiu/nodify/wiki"
            className="hover:text-primary-darker"
          >
            Docs
          </a>
          ·
          <a
            href="https://github.com/miroiu/nodify"
            className="hover:text-primary-darker"
          >
            Github
          </a>
          ·
          <a
            href="https://www.nuget.org/packages/Nodify"
            target="_blank"
            className="hover:text-primary-darker"
          >
            Nuget
          </a>
        </div>
        <p className="text-primary">Copyright © 2022 Emanuel Miroiu</p>
        <h6 className="text-sm">
          Designed with ❤️ by{' '}
          <a
            className="text-primary"
            href="https://github.com/MiroiuGabriel"
            target="_blank"
          >
            MiroiuGabriel
          </a>
        </h6>
      </footer>
    </div>
  );
}

export default App;
