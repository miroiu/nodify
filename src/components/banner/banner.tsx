import React from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useBaseUrl from '@docusaurus/useBaseUrl';
import styles from './styles.module.css';
import Logo from '@site/static/img/logo.svg';

const GitHubStars = props => {
	return (
		<iframe
			src="https://ghbtns.com/github-btn.html?user=miroiu&repo=nodify&type=star&count=true&size=large"
			frameBorder="0"
			scrolling="0"
			width="170"
			height="30"
			title="GitHub"
			{...props}
		/>
	);
};

const GetStarted = ({ className, ...props }) => {
	return (
		<Link
			className={clsx(
				'button button--outline button--primary button--lg',
				className
			)}
			to={useBaseUrl('docs/')}
			{...props}
		>
			Get Started
		</Link>
	);
};

const TagLine = () => {
	return (
		<h1 className="hero__title">
			Build
			<span className={styles.highlightText}> node-based </span>
			editors quickly, focus on your business
			<span className={styles.highlightText}> logic </span>
		</h1>
	);
};

const Header = () => {
	return (
		<header className="row">
			<div className={clsx('col col--8', styles.bannerText)}>
				<TagLine />
				<div className={styles.buttons}>
					<GetStarted className={styles.getStartedButton} />
					<GitHubStars className={styles.gitHubButton} />
				</div>
			</div>
			<div className="col col--4">
				<Logo height={200} className={styles.logo} />
			</div>
		</header>
	);
};

export const Banner = () => {
	return (
		<div className={clsx('hero', styles.banner)}>
			<div className="container">
				<Header />
			</div>
		</div>
	);
};
