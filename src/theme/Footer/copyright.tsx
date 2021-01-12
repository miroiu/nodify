import React from 'react';
import Link from '@docusaurus/Link';

const docusaurusGreen = { color: '#3ECC5F' };
const heroPatternsPurple = { color: '#9179BA' };

export const Copyright = () => {
	return (
		<div>
			<div style={{ color: '#FD5618' }}>
				Copyright © {new Date().getFullYear()} Emanuel Miroiu.
			</div>
			<div className="additionalCopyright">
				Built with{' '}
				<Link style={docusaurusGreen} to="https://v2.docusaurus.io/">
					Docusaurus
				</Link>{' '}
				· Circuit background by{' '}
				<Link
					style={heroPatternsPurple}
					to="https://www.heropatterns.com/"
				>
					Steve Schoger
				</Link>
			</div>
		</div>
	);
};
