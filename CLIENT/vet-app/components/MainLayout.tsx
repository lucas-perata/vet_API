import { useRouter } from 'next/router';
import { useSelector } from 'react-redux';

import { RootState } from '../store/store';
import Navbar from "../app/nav/Navbar"

type Props = {
  children: React.ReactNode;
};

const AUTHED_ROUTES = ['/authed'];

const MainLayout = ({ children }: Props) => {
  const router = useRouter();
  const isAuthedRoute = AUTHED_ROUTES.includes(router.pathname);
  const { token } = useSelector((state: RootState) => state.auth);

  if (!isAuthedRoute) return children;

  return (
    <div>
      <Navbar
        tokenExpiryDate={token} 
      />
      {children}
    </div>
  );
};

export default MainLayout;