import type { MiddlewareHandler } from 'astro';

/**
 * This middleware converts the pathname to lowercase and adds .html to the final path for documentation pages and redirects the request to the URL with a 301 status code.
 *
 * NOTE! This is running only for server side rendering.
 */
export const onRequest: MiddlewareHandler = async (context, next) => {
  const url = new URL(context.request.url);

  if (url.pathname.includes('wiki/') && !url.pathname.includes('.html')) {
    const redirectUrl = `${url.pathname.toLocaleLowerCase()}.html`;
    return next(redirectUrl);
  }

  return next();
};
