/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
	theme: {
		extend: {
			colors: {
				primary: '#fd5618',
				'primary-darker': '#e94102',
				secondary: '#662d91',
				card: '#24283788',
			},
			backgroundImage: {
				'grid-pattern':
					"data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 32 32' width='32' height='32' fill='none' stroke='rgb(241 245 249 / 0.03)'%3e%3cpath d='M0 .5H31.5V32'/%3e%3c/svg%3e",
			},
		},
	},
	plugins: [],
};
