import axios from 'axios';

// Tạo một instance của axios với cấu hình mặc định
const apiClient = axios.create({
  baseURL: 'http://localhost:5014/api', // Địa chỉ URL của API của bạn
  headers: {
    'Content-Type': 'application/json',
  },
});

// Thêm interceptor để thêm token vào headers nếu có
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`; // Thêm token vào headers
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

export default apiClient;
