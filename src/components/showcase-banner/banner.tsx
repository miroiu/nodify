import React from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import styles from './styles.module.css';

const SubmitApplication = ({ className, ...props }) => {
	return (
		<Link
			className={clsx(
				'button button--primary button--lg margin-top--lg',
				className
			)}
			href={props.url}
			{...props}
		>
			Submit yours
		</Link>
	);
};

const Header = ({ submitUrl }) => {
	return (
		<header className="row">
			<div className="col col--8">
				<h1 className="hero__title">
					Explore these awesome applications people are building with
					<span className={styles.highlightText}> Nodify</span> or
					<div>
						<SubmitApplication
							url={submitUrl}
							className={styles.addApplicationButton}
						/>
					</div>
				</h1>
			</div>
			<div className="col col--4" />
		</header>
	);
};

export const Banner = ({ submitUrl }) => {
	return (
		<div className={clsx('hero', styles.banner)}>
			<div className="container">
				<Header submitUrl={submitUrl} />
			</div>
		</div>
	);
};
