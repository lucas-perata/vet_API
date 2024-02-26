import LoginComponent from "./sessions/Login";
import List from "./pets/List";


export default function Home() {
  return (
    <div>
      <h3>Content</h3>
      <LoginComponent/>
      <List/>
    </div>
  );
}
