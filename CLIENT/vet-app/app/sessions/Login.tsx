"use client"

import React, { useState, FormEvent } from 'react';
import { loginOwner } from '../../utils/auth';
import { useToast } from "@/components/ui/use-toast"


const LoginComponent: React.FC = () => {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const { toast } = useToast()


    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const user = await loginOwner({ email, password });
            toast({
                title: "Ingresaste"              
            });
        } catch (err:any) {
            setError(err.message);

        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
            />
            <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
            />
            <button type="submit">Log in</button>
            {error && <p>{error}</p>}
            {successMessage && <p>{successMessage}</p>}

        </form>
    );
};

export default LoginComponent;