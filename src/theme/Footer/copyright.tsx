import React from 'react';
import Link from '@docusaurus/Link';
import './styles.css';

const DocusaurusLink = () => {
	return (
		<Link href="https://v2.docusaurus.io/">
			<span className="docusaurus-green">Docusaurus</span>
		</Link>
	);
};

const HeroPatternsLink = () => {
	return (
		<Link href="https://www.heropatterns.com/">
			<span className="heropatterns-purple">Steve Schoger</span>
		</Link>
	);
};

export const Copyright = () => {
	return (
		<div>
			<div className="copyright-color">
				Copyright © {new Date().getFullYear()} Emanuel Miroiu
			</div>
			<div className="additionalCopyright">
				Built with <DocusaurusLink /> · Circuit background by{' '}
				<HeroPatternsLink />
			</div>
		</div>
	);
};
