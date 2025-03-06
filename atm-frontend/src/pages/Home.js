import React from 'react';
import { useNavigate } from 'react-router-dom';

const Home = () => {
    const navigate = useNavigate(); // Khởi tạo navigate

    
    const customerId = null; 
    const balance = 0; 

    // Hàm điều hướng
    const handleNavigation = (path) => {
        navigate(path);
    };

    return (
        <div>
            <h1>Home Page</h1>
            {customerId ? (
                <div>
                    <p>Welcome, Customer ID: {customerId}!</p>
                    <p>Your balance: ${balance.toFixed(2)}</p> {/* Hiển thị số dư */}

                    <div>
                        <button onClick={() => handleNavigation('/change-password')}>Change Password</button>
                        <button onClick={() => handleNavigation('/withdraw')}>Withdraw</button>
                        <button onClick={() => handleNavigation('/deposit')}>Deposit</button>
                        <button onClick={() => handleNavigation('/transaction-history')}>Transaction History</button>
                        <button onClick={() => handleNavigation('/transfer')}>Transfer</button>
                    </div>
                </div>
            ) : (
                <div>
                    <p>Please log in to access more features.</p>
                    {/* Các nút cho đăng nhập và đăng ký */}
                    <button onClick={() => handleNavigation('/login')}>Login</button>
                    <button onClick={() => handleNavigation('/register')}>Register</button>
                </div>
            )}
        </div>
    );
};

export default Home;
