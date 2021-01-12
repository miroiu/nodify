import React from 'react';
import { FooterLinkItem } from '@docusaurus/theme-common';
import Link from '@docusaurus/Link';
import useBaseUrl from '@docusaurus/useBaseUrl';

export const FooterLink = ({ to, href, label, ...props }: FooterLinkItem) => {
	const toUrl = useBaseUrl(to);
	const normalizedHref = useBaseUrl(href, { forcePrependBaseUrl: true });

	return (
		<Link
			className="footer__link-item"
			{...(href
				? {
						target: '_blank',
						rel: 'noopener noreferrer',
						href: normalizedHref,
				  }
				: {
						to: toUrl,
				  })}
			{...props}
		>
			{label}
		</Link>
	);
};
