const FetchProductData = () => {
    return fetch('https://fakestoreapi.com/products')
        .then((response) => response.json())
        .then((json) => {
            console.log(json);
            return json;
        })
        .catch((error) => {
            console.error(error);
        });
}

export default FetchProductData;