import React from 'react';
import { useThemeConfig } from '@docusaurus/theme-common';
import clsx from 'clsx';
import './styles.css';
import { FooterLink } from './footer-link';
import { Copyright } from './copyright';

const links = [
	{
		label: 'Docs',
		to: 'docs/',
	},
	{
		label: 'Showcase',
		to: 'showcase/',
	},
	{
		label: 'Github',
		href: 'https://github.com/miroiu/nodify',
	},
	{
		label: 'Nuget',
		href: 'https://www.nuget.org/packages/Nodify',
	},
];

const Footer = () => {
	const { footer } = useThemeConfig();

	return (
		<footer
			className={clsx('footer', {
				'footer--dark': footer.style === 'dark',
			})}
		>
			<div className="container text--center">
				<div className="footer__links">
					{links.map((item, index) => {
						return (
							<React.Fragment key={item.href || item.to}>
								<FooterLink {...item} />
								{index < links.length - 1 && (
									<span className="footer__link-separator">
										·
									</span>
								)}
							</React.Fragment>
						);
					})}
				</div>
				<Copyright />
			</div>
		</footer>
	);
};

export default Footer;
