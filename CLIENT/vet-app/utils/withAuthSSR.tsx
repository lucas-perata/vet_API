import { redirect } from "next/navigation";

const withAuthSSR = (kookies: string | undefined) => {
  if (kookies == null) {
    redirect("/sessions/login");
  }
};

export default withAuthSSR;
