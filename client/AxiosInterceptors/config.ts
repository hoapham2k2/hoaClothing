// config axios interceptor in order to add token to request header and handle error response from server
import axios from 'axios';

//thiết kế interceptor
const axiosInstance = axios.create({
    baseURL: `${
        //kiểm tra môi trường là dev hay prod để lấy đúng url
        process.env.NODE_ENV === 'development' ? process.env["NEXT_PUBLIC_API_URL--DEVELOPMENT"] : process.env["NEXT_PUBLIC_API_URL--PRODUCTION"]
    }`,
    timeout: 10000, // time out là thời gian chờ phản hồi từ server nếu quá thời gian này thì sẽ báo lỗi
    headers: {
        'Content-Type': 'application/json',
    },
});

// Add a request interceptor
axiosInstance.interceptors.request.use(
    function (config) {
        // Do something before request is sent
        // config.headers['Authorization'] = `Bearer ${token}`;

        //chỉ một số request bắt đầu bằng /api/Auth thì mới thêm token vào header

        if (config.url?.startsWith('/api/Auth')) {
            //lấy token từ local storage
            const token = localStorage.getItem('token');
            //nếu có token thì thêm vào header
            if (token) {
                console.log("token", token);
                config.headers['Authorization'] = `Bearer ${token}`;
            }

            //nếu không có token thì chuyển về trang login
            else {
                console.log("haven't token")
                window.location.href = '/login';
            }
        }
        console.log("Request interceptor", config);
        return config;
    }
);

// Add a response interceptor
axiosInstance.interceptors.response.use(
    function (response) {

        console.log("Response interceptor", response);
        return response;
    }
);

export default axiosInstance;