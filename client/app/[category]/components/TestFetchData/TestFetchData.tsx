'use client'
import React, {FunctionComponent, useEffect} from 'react';
import FetchProductData from "@/services/FetchProductData";

interface OwnProps {
}

type Props = OwnProps;

const TestFetchData: FunctionComponent<Props> = (props) => {
    const [data, setData] = React.useState(null);

    useEffect(() => {
        FetchProductData().then((res) => {
            setData(res.data);
        })
    }, []);

    return (
        <div>
            {
                data ? <h1>{data}</h1> : <h1>Loading...</h1>
            }
        </div>

    );
};

export default TestFetchData;
