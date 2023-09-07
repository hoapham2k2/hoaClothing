'use client'

import React, {FunctionComponent, useEffect} from 'react';
import styles from './ShopDropDownModal.module.css'
import {AiOutlineCloseCircle} from "react-icons/ai";
import {MdExpandLess} from "react-icons/md";
import Link from "next/link";

interface OwnProps {
    modalRef: React.RefObject<HTMLDivElement>
}

type Props = OwnProps;

interface ISubCategory {
    id: number,
    name: string,
}

interface ICategory {
    id: number,
    name: string,
    subCategory: ISubCategory[]
}

const listCategory: ICategory[] = [
    {
        id: 1,
        name: 'Tops',
        subCategory: [
            {
                id: 1,
                name: 'Tee',
            },
            {
                id: 2,
                name: 'Polo',
            },
            {
                id: 3,
                name: 'Baby Tee',
            },
            {
                id: 4,
                name: 'All',
            }
        ]
    },
    {
        id: 2,
        name: 'Outwears',
        subCategory: [
            {
                id: 1,
                name: 'Jacket',
            }, {
                id: 2,
                name: 'Hoodie',
            }, {
                id: 3,
                name: 'Varsity',
            }, {
                id: 4,
                name: 'All',
            }
        ]
    },
    {
        id: 3,
        name: 'Bottoms',
        subCategory: [
            {
                id: 1,
                name: 'Short',
            },
            {
                id: 2,
                name: 'Pant',
            },
            {
                id: 3,
                name: 'All',
            }
        ]
    },
    {
        id: 4,
        name: 'Accessories',
        subCategory: [
            {
                id: 1,
                name: 'Wallet',
            }, {
                id: 2,
                name: 'Cap',
            }, {
                id: 3,
                name: 'Backpacks',
            }, {
                id: 4,
                name: 'All',
            }
        ]
    },
]

const ShopDropDownModal: FunctionComponent<Props> = (props) => {

    useEffect(() => {
        //thêm sự kiện click cho các category
        const listCategory = document.querySelectorAll(`.${styles.shopDropDownModal__body__wrapper__category}`);
        listCategory.forEach((category) => {
                category.addEventListener('click', (e) => {
                    category.classList.toggle(`${styles.shopDropDownModal__body__wrapper__category__active}`);

                    const subCategory = category.querySelector(`.${styles.shopDropDownModal__body__wrapper__category__subCategory}`); //lấy ra subCategory của category đó

                    //kiểm tra nếu category đó có class active thì hiển thị subCategory
                    if (category.classList.contains(`${styles.shopDropDownModal__body__wrapper__category__active}`)) {
                        subCategory?.classList.add(`${styles.shopDropDownModal__body__wrapper__category__subCategory__active}`);
                    } else {
                        subCategory?.classList.remove(`${styles.shopDropDownModal__body__wrapper__category__subCategory__active}`);
                    }
                })
            }
        )
    }, [])
    return (
        <div>
            <div className={styles.shopDropDownModal} ref={props.modalRef} onClick={(e) => {
                if (e.target === props.modalRef.current) {
                    props.modalRef.current?.style.setProperty('display', 'none');
                }
            }}>
                <div className={styles.shopDropDownModal__wrapper}>
                    <div className={styles.shopDropDownModal__header}>
                        <span style={{
                            display: 'flex',
                            alignItems: 'center',
                            cursor: 'pointer',
                            gap: '5px'
                        }} onClick={() => {
                            props.modalRef.current?.style.setProperty('display', 'none');
                        }}>
                            <AiOutlineCloseCircle className={styles.shopDropDownModal__header__icon}/>
                            <span>Đóng</span>
                        </span>
                    </div>
                    <div className={styles.shopDropDownModal__body}>
                        <div className={styles.shopDropDownModal__body__wrapper}>
                            {
                                listCategory.map((category, index) =>
                                    <div key={category.id} className={styles.shopDropDownModal__body__wrapper__category}
                                         id={`${category.id}`}>
                                        <div className={styles.shopDropDownModal__body__wrapper__category__name}>
                                            <Link href={`/${category.name.toLowerCase()}`} onClick={() => {
                                                props.modalRef.current?.style.setProperty('display', 'none');
                                            }}>
                                                {category.name}
                                            </Link>
                                            <MdExpandLess
                                                className={styles.shopDropDownModal__body__wrapper__category__name__icon}/>
                                        </div>
                                        <div className={styles.shopDropDownModal__body__wrapper__category__subCategory}>
                                            {
                                                category.subCategory.map((subCategory, index) =>
                                                    <Link
                                                        href={`/${category.name.toLowerCase()}/${subCategory.name.toLowerCase()}`}
                                                        key={index}
                                                        className={styles.shopDropDownModal__body__wrapper__category__subCategory__name}
                                                        onClick={() => {
                                                            props.modalRef.current?.style.setProperty('display', 'none');
                                                        }}
                                                    >
                                                        {subCategory.name}
                                                    </Link>
                                                )
                                            }
                                        </div>
                                    </div>
                                )
                            }
                        </div>
                    </div>
                    <div className={styles.shopDropDownModal__footer}></div>
                </div>
            </div>
        </div>
    );
};

export default ShopDropDownModal;
