import React, {FunctionComponent} from 'react';

interface OwnProps {
    children: React.ReactNode
}

type Props = OwnProps;

const layout: FunctionComponent<Props> = (props) => {

    return (
        <div>
            {props.children}
        </div>

    );
};

export default layout;
