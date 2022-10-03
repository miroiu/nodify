import { apps } from './apps';
import { GithubButton } from './components';

const features = [
  {
    title: '⭐ Built for MVMM',
    description: 'Designed from the ground up to work with MVVM',
  },
  {
    title: '⭐ Performant',
    description: 'Optimized for interaction with hundreds of nodes at once',
  },
  {
    title: '⭐ Customizable',
    description:
      'Use the built-in themes or create your own with the power of XAML',
  },
  {
    title: '⭐ Featureful',
    description: 'Fully featured navigation, zoom and area selection system',
  },
];

function App() {
  return (
    <div>
      <div className="max-w-screen-lg mx-auto px-4">
        <section className="h-screen flex flex-col items-center justify-center gap-8 relative">
          <div className="flex gap-20">
            <div className="flex flex-col gap-16 text-center sm:text-left">
              <div className="flex flex-col gap-4">
                <h2 className="relative uppercase text-4xl text-secondary font-extrabold mx-auto sm:mx-0">
                  Nodify
                  <span className="absolute top-1 left-1 uppercase text-4xl text-card font-extrabold -z-10">
                    Nodify
                  </span>
                </h2>
                <h1 className="text-5xl sm:text-6xl tracking-tight font-extrabold">
                  Build <span className="text-primary">node-based</span> editors
                  quickly, focus on your business{' '}
                  <span className="text-primary">logic</span>.
                </h1>
              </div>

              <div className="flex gap-6 items-center flex-col sm:flex-row">
                <a
                  className="hover:bg-primary bg-primary-darker transition-colors duration-200 ease-in-out py-4 px-6 font-bold text-3xl uppercase rounded-lg shadow-sm"
                  href="https://github.com/miroiu/nodify/wiki/Getting-Started"
                >
                  Get started
                </a>
                <GithubButton owner="miroiu" repo="nodify" />
              </div>
            </div>

            <img
              src="logo.svg"
              alt="Nodify logo"
              className="w-64 h-64 hover:scale-105 transition-all duration-200 ease-in-out hidden sm:block"
            />
            <div className="absolute bottom-0 w-full flex flex-col justify-center items-center sm:py-6">
              <a
                href="#showcase"
                className="flex flex-col items-center justify-center group"
              >
                <p className="text-xl mb-2 font-semibold opacity-75 group-hover:opacity-100 transition-opacity duration-200 ease-in-out">
                  Take a look
                </p>
                <img
                  src="img/carret.svg"
                  alt="Scroll to featured projects"
                  className="h-12 w-12 animate-bounce"
                />
              </a>
            </div>
          </div>
        </section>

        <section
          id="showcase"
          className="scroll-m-4 pt-10 sm:pt-28 flex flex-col gap-28"
        >
          <main className="relative flex flex-col gap-4 sm:grid sm:grid-cols-4 sm:gap-6">
            {features.map((feature, index) => (
              <section
                key={index}
                className="px-2 py-4 rounded-lg bg-gradient-to-r from-primary-darker to-secondary shadow-lg flex justify-center group cursor-feature hover:via-secondary"
              >
                <h1 className="text-xl font-semibold uppercase">
                  {feature.title}
                </h1>
                <p className="bg-secondary absolute top-full mt-2 rounded-lg w-full left-0 flex items-center text-center p-4 [clip-path:polygon(0_0,_100%_0,_100%_0,_0_0)] group-hover:[clip-path:polygon(0_0,_100%_0,_100%_100%,_0_100%)] transition-all duration-200">
                  {feature.description}
                </p>
              </section>
            ))}
          </main>

          <div className="sm:grid sm:grid-cols-2 flex flex-col gap-8">
            {apps.map((app, index) => (
              <div
                key={index}
                className="rounded-lg bg-gradient-to-t from-secondary via-secondary to-primary p-1 shadow-2xl overflow-hidden flex flex-col"
              >
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
                        target="_blank"
                        className="absolute top-0 right-0 w-full h-full [clip-path:circle(0%_at_100%_0)] group-hover:[clip-path:circle(150%_at_100%_0)] motion-safe:transition-all duration-200 bg-secondary opacity-90 flex justify-center items-center"
                      >
                        <img
                          src="img/external-link.svg"
                          alt="Go to project website"
                          className="w-12 h-12"
                        />
                      </a>
                    </div>
                  )}
                </div>
                <div className="bg-card backdrop-blur flex flex-col p-4 rounded-tr-md rounded-br-md grow">
                  <p className="text-xl font-extrabold mb-2">{app.title}</p>
                  {app.description && <p>{app.description}</p>}
                </div>
              </div>
            ))}
            <div className="rounded-lg bg-gradient-to-t from-secondary via-secondary to-primary p-1 shadow-2xl overflow-hidden flex flex-col">
              <a
                href="https://github.com/miroiu/nodify/edit/docs/src/apps.ts"
                target="_blank"
                className="h-full bg-secondary opacity-90 flex justify-center items-center group"
              >
                <img
                  src="img/plus.svg"
                  alt="Add your own project"
                  className="w-16 h-16 my-6 sm:group-hover:w-20 sm:group-hover:h-20 transition-all"
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
        <p className="text-sm">
          Designed with ❤️ by{' '}
          <a
            className="text-primary hover:text-primary-darker"
            href="https://github.com/MiroiuGabriel"
            target="_blank"
          >
            MiroiuGabriel
          </a>
        </p>
      </footer>
    </div>
  );
}

export default App;
