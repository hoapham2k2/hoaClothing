'use client';
import React, {FunctionComponent, useEffect, useState} from 'react';
import {MdDarkMode, MdLightMode} from "react-icons/md";
import styles from './ButtonToggleScheme.module.css'

// import {MdDarkMode, MdLightMode} from 'react-icons/md';
interface OwnProps {
}

type Props = OwnProps;

const ButtonToggleScheme: FunctionComponent<Props> = (props) => {
    const [scheme, setScheme] = useState('light');
    const toggleScheme = () => {
        if (scheme === 'light') {
            setScheme('dark');
        } else {
            setScheme('light');
        }
    }

    useEffect(() => {
        if (scheme === 'dark') {
            document.documentElement.setAttribute('data-theme', 'dark');
        } else {
            document.documentElement.setAttribute('data-theme', 'light');
        }
    }, [scheme]);

    return (
        <button className={styles.button__wrapper} onClick={toggleScheme}>
            {scheme === 'light' ? <MdDarkMode/> : <MdLightMode/>}
        </button>

    )
};

export default ButtonToggleScheme;
