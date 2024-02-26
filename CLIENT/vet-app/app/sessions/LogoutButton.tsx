"use client"
import { Button } from '@/components/ui/button';
import { logout } from '../../utils/auth';
import { useToast } from '@/components/ui/use-toast';

function LogoutButton() {
    const { toast } = useToast()
  const handleLogout = () => {
    logout();
    toast({
        title: "Cerraste tu sesión"              
    });
  };

  return (
    <Button onClick={handleLogout}>Logout</Button>
  );
}

export default LogoutButton;