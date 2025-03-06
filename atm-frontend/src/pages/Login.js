import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import PropTypes from 'prop-types';

const Login = ({ onLoginSuccess = () => {} }) => { 
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('http://localhost:5014/api/auth/login', {
                Email: email,
                password: password,
            });

            console.log('Login successful', response.data);
            onLoginSuccess(response.data);
            navigate('/'); 
        } catch (error) {
            console.error('Login error:', error);
            setError('Login failed, please try again.'); 
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email:</label>
                    <input 
                        type="text"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)} 
                        required 
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input 
                        type="password" 
                        value={password}
                        onChange={(e) => setPassword(e.target.value)} 
                        required 
                    />
                </div>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type="submit">Login</button>
            </form>
            <p>Don't have an account? <button onClick={() => navigate('/register')}>Register now!</button></p>
        </div>
    );
};

// Kiểm tra kiểu dữ liệu cho props
Login.propTypes = {
    onLoginSuccess: PropTypes.func,
};

export default Login;
