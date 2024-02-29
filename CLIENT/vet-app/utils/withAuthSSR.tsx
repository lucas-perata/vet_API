import { redirect } from 'next/navigation';

const withAuthSSR = (kookies: string) => {
  if(kookies == null) {
    redirect('/sessions/login');
    };
  
}

export default withAuthSSR;