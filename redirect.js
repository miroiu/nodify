const url = window.location;

if (url.pathname.includes('wiki/') && !url.pathname.includes('.html')) {
  const redirectUrl = `${url.pathname.toLocaleLowerCase()}.html`;
  window.location.href = redirectUrl;
}
