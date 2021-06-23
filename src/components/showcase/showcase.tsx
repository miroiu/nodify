import React from 'react';
import apps from '../../showcase/apps';
import { ApplicationCardList } from '../application-card/card-list';
import { SubmitApplication } from '../showcase-banner/banner';

const SUBMIT_APP_URL =
	'https://github.com/miroiu/nodify/edit/docs/src/showcase/apps.ts';

const exampleApps = apps.filter(a => a.category === 'example-app');
const otherApps = apps.filter(a => a.category !== 'example-app');

export const Showcase = () => {
	return (
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
			<SubmitApplication url={SUBMIT_APP_URL} />
		</main>
	);
};
