'use client';
import React, {FunctionComponent, useEffect} from 'react';
import styles from './Header.module.css'
import {FaList} from "react-icons/fa";
import {AiOutlineShoppingCart} from "react-icons/ai";
import ShopDropDownModal from "@/app/components/header/shopDropDownModal/ShopDropDownModal";
import {useParams} from "next/navigation";
import Link from "next/link";

interface OwnProps {

}

type Props = OwnProps;

const Header: FunctionComponent<Props> = (props) => {
    const modalRef = React.useRef<HTMLDivElement>(null);
    const headerRef = React.useRef<HTMLDivElement>(null);
    const pageParams = useParams();
    console.log("pageParams", pageParams);

    useEffect(() => {
        const iconList = document.querySelector(`.${styles.header__left__icon}`);
        iconList?.addEventListener('click', () => {
                modalRef.current?.style.setProperty('display', 'grid');
            }
        );
    }, []);

    useEffect(() => {
        //browser lắng nghe sự kiện scroll và thực hiện hàm handleScroll
        window.addEventListener('scroll', handleScroll);

        //trả về hàm để clear event listener
        return () => {
            window.removeEventListener('scroll', handleScroll);
        }
    }, []);

    const handleScroll = () => {
        //nếu yScroll > 0 thì set background cho header
        //nếu yScroll = 0 thì set background cho header

        const yScroll = window.scrollY;
        headerRef.current?.style.setProperty('transition', 'all 0.3s ease-in-out');
        if (yScroll > 0) {
            headerRef.current?.style.setProperty('box-shadow', '0 0 10px rgba(0,0,0,0.2)');
        } else {
            headerRef.current?.style.setProperty('box-shadow', 'none');
        }
    }


    return (
        <header className={styles.header}>
            <div className={styles.header__wrapper} style={
                //check pageParams, if pageParams is empty object, then set display to none
                Object.keys(pageParams).length === 0 ? {
                    //config for home page
                    backgroundColor: 'transparent',
                    color: 'whitesmoke',
                } : {
                    //config for other page
                    backgroundColor: 'white',
                    color: 'black',
                }
            } ref={headerRef}>
                <div className={styles.header__left}>
                    <FaList className={styles.header__left__icon}/>
                </div>
                <div className={styles.header__center}>
                    <Link href={`/`} style={{
                        color: 'inherit',
                        textDecoration: 'inherit',
                    }}>
                        Hoà
                    </Link>
                </div>
                <div className={styles.header__right}>
                    <AiOutlineShoppingCart className={styles.header__right__icon}/>
                </div>
            </div>
            <ShopDropDownModal modalRef={modalRef}/>
        </header>
    );
};

export default Header;
