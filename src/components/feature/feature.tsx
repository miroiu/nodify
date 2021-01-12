import React from 'react';
import styles from './styles.module.css';

export const Feature = ({ label, content }) => {
	return (
		<div className={styles.feature}>
			<div className={styles.header}>
				<h3>{label}</h3>
			</div>
			<div className={styles.body}>
				<p>{content}</p>
			</div>
		</div>
	);
};
