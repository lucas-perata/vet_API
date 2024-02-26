"use client"
import React, { useState, FormEvent } from 'react';
import { registerOwner, registerVet } from '../../../utils/auth';
import { useToast } from "@/components/ui/use-toast"
import { CardTitle, CardDescription, CardHeader, CardContent, Card } from "@/components/ui/card"
import { Label } from "@/components/ui/label"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { Switch } from "@/components/ui/switch"




const page: React.FC = () => {
    const [isSwitchOn, setSwitchOn] = useState(false); 

    const handleSwitchChange = () => {
        setSwitchOn(!isSwitchOn);
    };

    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [displayName, setDisplayName] = useState<string>("");
    const [error, setError] = useState<string | null>(null);
    const { toast } = useToast()


    const handleSubmitOwner = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const user = await registerOwner({ email, password, displayName });
            toast({
                title: "Ingresaste"              
            });
            window.location.href = "/dashboard"
        } catch (err:any) {
            setError(err.message);
            console.log(error);
            toast({
                title: "Error"})
        }
    };

    const handleSubmitVet = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const user = await registerVet({ email, password, displayName });
            toast({
                title: "Ingresaste"              
            });
            window.location.href = "/dashboard"
        } catch (err:any) {
            setError(err.message);
            console.log(error);
            toast({
                title: "Error"})
        }
    };

    return ( 
        <>
 <p>{isSwitchOn ? 'Switch is ON' : 'Switch is OFF'}</p> 

        <form onSubmit={isSwitchOn ? handleSubmitVet : handleSubmitOwner}>    
            <Card className="mx-auto max-w-sm">
            <CardHeader className="space-y-1">
                <CardTitle className="text-2xl font-bold">Crea tu cuenta</CardTitle>
                    <div className='flex justify-center align-middle'>
                        <Switch id="registrate como veterinario" onClick={handleSwitchChange}/>
                        <Label htmlFor="registrate como veterinario">Registrate como veterinario</Label>
                    </div>
            </CardHeader>
            <CardContent>
                <div className="space-y-4">
                <div className="space-y-2">
                    <Label htmlFor="string">Username</Label>
                    <Input type="text"
                        value={displayName}
                        onChange={(e) => setDisplayName(e.target.value)}
                        id="displayName" placeholder="juan" required />
                </div>
                <div className="space-y-2">
                    <Label htmlFor="email">Email</Label>
                    <Input type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        id="email" placeholder="m@example.com" required />
                </div>
                <div className="space-y-2">
                    <Label htmlFor="password">Password</Label>
                    <Input id="password" required type="password" 
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Password" />
                </div>
                {error && <p className="text-red-500">{error}</p>}

                <Button className="w-full" type="submit">
                    Crear cuenta
                </Button>
                </div>
            </CardContent>
            </Card>
        </form>
        </>
    );
};

export default page;


