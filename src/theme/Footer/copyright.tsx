import Link from '@docusaurus/Link';
import React from 'react';

export const Copyright = () => {
	return (
		<div>
			<div style={{ color: '#FD5618' }}>
				Copyright © {new Date().getFullYear()} Emanuel Miroiu.
			</div>
			<div className="additionalCopyright">
				Built with{' '}
				<Link
					style={{ color: '#3ECC5F' }}
					to="https://v2.docusaurus.io/"
				>
					Docusaurus
				</Link>{' '}
				· Circuit background by{' '}
				<Link
					style={{ color: '#9179BA' }}
					to="https://www.heropatterns.com/"
				>
					Steve Schoger
				</Link>
			</div>
		</div>
	);
};
