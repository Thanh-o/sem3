import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

const ChangePassword = () => {
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const navigate = useNavigate();
  const customerId = parseInt(localStorage.getItem('customerId'), 10);

  // Kiểm tra đăng nhập khi tải component
  useEffect(() => {
    if (!customerId) {
      toast.error('Bạn chưa đăng nhập. Vui lòng đăng nhập để đổi mật khẩu.');
      navigate('/login');
    }
  }, [customerId, navigate]);

  const handleChangePassword = async (e) => {
    e.preventDefault();

    if (!oldPassword || !newPassword) {
      toast.error('Vui lòng điền đầy đủ thông tin.');
      return;
    }

    try {
      await axios.post('http://localhost:5014/api/auth/change-password', {
        customerId,
        oldPassword,
        newPassword,
      });
      toast.success('Đổi mật khẩu thành công!');
      navigate('/login');
    } catch (error) {
      console.error('Error changing password:', error.response ? error.response.data : error.message);
      if (error.response) {
        const errorMessages = error.response.data.errors;
        toast.error('Đổi mật khẩu thất bại: ' + JSON.stringify(errorMessages));
      } else {
        toast.error('Đã xảy ra lỗi. Vui lòng thử lại sau.');
      }
    }
  };

  return (
    <div className="container mt-5">
      <h2 className="text-center">Đổi Mật Khẩu</h2>
      <form onSubmit={handleChangePassword} className="w-50 mx-auto">
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            placeholder="Mật khẩu cũ"
            value={oldPassword}
            onChange={(e) => setOldPassword(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            placeholder="Mật khẩu mới"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
          />
        </div>
        <button type="submit" className="btn btn-primary w-100">Đổi Mật Khẩu</button>
      </form>
    </div>
  );
};

export default ChangePassword;
