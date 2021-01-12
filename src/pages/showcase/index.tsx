import React from 'react';
import Layout from '@theme/Layout';
import styles from './styles.module.css';
import apps from '../../showcase/apps';
import { Banner } from '../../components/showcase-banner/banner';
import { ApplicationCardList } from '../../components/application-card/card-list';

const SUBMIT_APP_URL =
	'https://github.com/miroiu/nodify/edit/docs/src/showcase/apps.ts';

const exampleApps = apps.filter(a => a.category === 'example-app');
const otherApps = apps.filter(a => a.category !== 'example-app');

const Showcase = () => {
	return (
		<Layout
			title="Showcase"
			description="Explore these awesome applications people are building with Nodify"
			wrapperClassName={styles.fullWrapper}
		>
			<Banner submitUrl={SUBMIT_APP_URL} />
			<main className="container">
				{otherApps.length > 0 && (
					<ApplicationCardList
						id="all"
						name="Your applications"
						apps={otherApps}
					/>
				)}
				<ApplicationCardList
					id="examples"
					name="Example applications"
					apps={exampleApps}
				/>
			</main>
		</Layout>
	);
};

export default Showcase;
