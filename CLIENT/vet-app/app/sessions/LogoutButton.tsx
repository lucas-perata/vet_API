"use client"
import { Button } from '@/components/ui/button';
import { logout } from '../../utils/auth';
import { useToast } from '@/components/ui/use-toast';
import useStore from '@/store/store';

function LogoutButton() {
  const removeToken = useStore((state) => state.removeToken);
  const { toast } = useToast()
  
  const handleLogout = () => {
    removeToken();
    toast({
        title: "Cerraste tu sesión"              
    });
    window.location.href = '/';
  };

  return (
    <Button onClick={handleLogout}>Logout</Button>
  );
}

export default LogoutButton;