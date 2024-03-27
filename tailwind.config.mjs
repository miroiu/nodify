/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{astro,html,js,jsx,md,mdx,svelte,ts,tsx,vue}'],
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
      cursor: {
        feature: `url("data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='40' height='48' viewport='0 0 100 100' style='fill:black;font-size:24px;'><text y='50%'>⭐</text></svg>") 16 0, auto`,
      },
    },
  },
  plugins: [],
};
