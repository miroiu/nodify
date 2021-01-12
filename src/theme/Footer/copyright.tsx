import React from 'react';
import './styles.css';

export const Copyright = () => {
	return (
		<div>
			<div className="copyright-color">
				Copyright © {new Date().getFullYear()} Emanuel Miroiu.
			</div>
			<div className="additionalCopyright">
				Built with{' '}
				<a href="https://v2.docusaurus.io/" target="_blank">
					<span className="docusaurus-green">Docusaurus</span>
				</a>{' '}
				· Circuit background by{' '}
				<a href="https://www.heropatterns.com/" target="_blank">
					<span className="heropatterns-purple">Steve Schoger</span>
				</a>
			</div>
		</div>
	);
};
