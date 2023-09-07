'use client';
import React, {FunctionComponent} from 'react';
import {Swiper, SwiperSlide} from 'swiper/react';
import styles from './PageCarousel.module.css'
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';
import {Mousewheel, Pagination} from "swiper/modules";
import Image from "next/image";

interface OwnProps {
}

type Props = OwnProps;

const listURLImagePreview = [
    "/slider01.jpg",
    "/slider02.jpg",
    "/slider03.jpg",
]

const PageCarousel: FunctionComponent<Props> = (props) => {

    return (

        <Swiper
            className={styles.pageCarousel}
            slidesPerView={1}
            loop={true}
            direction={'vertical'}
            pagination={{
                clickable: true,
            }}
            mousewheel={{
                releaseOnEdges: true, // cho phép scroll chuột khi đến cuối slide (slide cuối cùng thì scroll chuột sẽ chuyển sang slide đầu tiên)
            }}

            modules={[Pagination, Mousewheel]}
        >
            {
                listURLImagePreview.map((urlImagePreview, index) =>
                    <SwiperSlide key={index}>
                        <Image src={urlImagePreview} alt="previewSlider" width={1920} height={1080}/>
                    </SwiperSlide>
                )
            }
        </Swiper>
    );
};

export default PageCarousel;
