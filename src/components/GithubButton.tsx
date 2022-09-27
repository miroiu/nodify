import { type FC, useEffect, useState } from 'react';

type GithubButtonProps = {
	owner: string;
	repo: string;
};

export const GithubButton: FC<GithubButtonProps> = ({ owner, repo }) => {
	const [stars, setStars] = useState<number>();

	useEffect(() => {
		const getCount = async () => {
			const res = await fetch(
				`https://api.github.com/repos/${owner}/${repo}`
			);
			const data = await res.json();
			setStars(data.stargazers_count);
		};
		getCount();
	}, []);

	return (
		<a
			className="flex bg-white hover:bg-slate-100 transition-colors duration-200 ease-in-out w-fit text-black px-3 py-2 rounded-full gap-2 cursor-pointer font-medium"
			href="https://github.com/miroiu/nodify"
		>
			<img src="img/github_logo.svg" className="w-6 h-6" />
			Star
			<span className="border-l min-w-[45px] px-2">{stars}</span>
		</a>
	);
};
