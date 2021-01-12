import React from 'react';
import clsx from 'clsx';
import Image from '@theme/IdealImage';
import Link from '@docusaurus/Link';
import styles from './styles.module.css';

export type ApplicationCardProps = {
	picture: any;
	title: string;
	website?: string;
	description?: string;
	source?: string;
};

export const ApplicationCard = ({
	picture,
	title,
	website,
	description,
	source,
}: ApplicationCardProps) => {
	return (
		<div className={clsx('card', styles.cardContainer)}>
			<div className={clsx('card__image', styles.image)}>
				<Image img={picture} alt={title} />
			</div>
			<div className="card__body">
				<h3 className={styles.title}>{title}</h3>
				<p className="avatar__subtitle">{description}</p>
			</div>
			<div className="card__footer">
				<div className="button-group button-group--block">
					{website && (
						<Link
							href={website}
							className={clsx(
								'button button--small button--block',
								styles.button,
								styles.outline
							)}
						>
							Website
						</Link>
					)}
					{source && (
						<Link
							href={source}
							className={clsx(
								'button button--small button--primary button--block',
								styles.button
							)}
						>
							Source
						</Link>
					)}
				</div>
			</div>
		</div>
	);
};
