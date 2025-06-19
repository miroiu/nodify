// import type { MiddlewareHandler } from 'astro';

// /**
//  * This middleware converts the pathname to lowercase and removes any trailing slashes for documentation pages and redirects the request to the normalized URL with a 301 status code.
//  */
// export const onRequest: MiddlewareHandler = async ({ request }, next) => {
//   const url = new URL(request.url);

//   if (url.pathname.includes('/wiki')) {
//     const normalizedPath = url.pathname.toLowerCase().replace(/\/$/, '');

//     if (url.pathname !== normalizedPath) {
//       const redirectUrl = `${normalizedPath}${url.search}`;
//       return new Response(null, {
//         status: 301,
//         headers: {
//           Location: redirectUrl,
//         },
//       });
//     }
//   }

//   return next();
// };
