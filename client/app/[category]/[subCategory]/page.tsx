import React, {FunctionComponent} from 'react';
import styles from "./subCategory.module.css"

interface OwnProps {

}

type Props = OwnProps;

const page: FunctionComponent<Props> = (props) => {
    
    return (
        <main className={styles.page}>
            <h1>This come from sub category</h1>
        </main>
    );
};

export default page;
