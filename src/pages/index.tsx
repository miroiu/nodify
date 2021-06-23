import React from 'react';
import Layout from '@theme/Layout';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import { Banner } from '../components/banner/banner';
import styles from './index.module.css';
import Link from '@docusaurus/Link';
import { Feature } from '../components/feature/feature';
import { Showcase } from '../components/showcase/showcase';
import clsx from 'clsx';

const features = [
	// {
	// 	label: 'No dependencies',
	// 	content: 'The entire editor is made from scratch using C# and WPF',
	// },
	{
		label: 'Built for MVVM',
		content: 'Designed from the ground up to work with MVVM',
	},
	{
		label: 'High performance',
		content: 'Optimized for interaction with hundreds of nodes at once.',
	},
	{
		label: 'Customizable',
		content: 'Modify the controls appearance with the power of XAML',
	},
	{
		label: 'Extensible',
		content: 'Customize behavior by extending the base controls',
	},
	{
		label: 'Themeable',
		content: 'Use the built-in light and dark themes or create your own',
	},
	{
		label: 'Accessible',
		content: 'Fully featured navigation, zoom and area selection systems',
	},
];

const Home = () => {
	const context = useDocusaurusContext();
	const { siteConfig = {} } = context;

	return (
		<Layout
			permalink="/"
			title={siteConfig.tagline}
			description="A high-performance controls library that makes it easy to build node-based editors"
			wrapperClassName={styles.fullWrapper}
		>
			<Banner />
			<Showcase />
			<main className={clsx(styles.main, 'margin-top--xl')}>
				<div className={clsx('container', styles.featuresContainer)}>
					{features.map(feature => (
						<Feature
							key={feature.label}
							label={feature.label}
							content={feature.content}
						/>
					))}
				</div>
			</main>
		</Layout>
	);
};

export default Home;
