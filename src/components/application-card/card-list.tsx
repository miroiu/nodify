import React from 'react';
import { ApplicationInfo } from '../../showcase/apps';
import { ApplicationCard } from './card';
import styles from './styles.module.css';

type ApplicationCardListProps = {
	id: string;
	name: string;
	apps: ApplicationInfo[];
};

export const ApplicationCardList = ({
	id,
	name,
	apps,
	...props
}: ApplicationCardListProps) => {
	return (
		<section {...props}>
			<h2 id={id} className={styles.listName}>
				{name}
				<a href={`#${id}`} className="hash-link">
					#
				</a>
			</h2>
			<div className="row">
				{apps.map(appInfo => (
					<div
						key={appInfo.title}
						className="col col--4 margin-bottom--lg"
					>
						<ApplicationCard
							{...appInfo}
							picture={appInfo.preview}
						/>
					</div>
				))}
			</div>
		</section>
	);
};
