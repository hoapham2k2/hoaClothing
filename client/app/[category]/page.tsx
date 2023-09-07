import React, {FunctionComponent} from 'react';
import styles from "./category.module.css"
import TestFetchData from "@/app/[category]/components/TestFetchData/TestFetchData";

interface OwnProps {
}

type Props = OwnProps;

const page: FunctionComponent<Props> = (props) => {

    return (
        <div className={styles.page}>
            <div style={{
                height: '3000px'
            }}>alo
            </div>
            <TestFetchData/>
        </div>
    );
};

export default page;
