
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HouseList from '../house/HouseList';
import './App.css';
import Header from './Header';
import HouseDetail from '../house/HouseDetail';
import HouseAdd from '../house/HouseAdd';
import HouseEdit from '../house/HouseEdit';
import Contact from '../contact/contact';
import useFetchUser from "../hooks/UserHooks";
import config from "../../config";

function App() {
  const { isSuccess } = useFetchUser();
  const loginUrl = `${config.baseApiUrl}/account/login`;
  const logout = `${config.baseApiUrl}/account/logout`

  return (
    <BrowserRouter>
      <div className='container' >     
        {!isSuccess && <a href={loginUrl}>Login</a>}
        {isSuccess && <a href={logout}>Logout</a>}
        <Header subtitle='Providing houses all over the world'></Header>
        <Routes>
         <Route path="/" element={<HouseList/>} > </Route>
         <Route path="/house/:id" element={<HouseDetail />}></Route>   
         <Route path="/house/add" element={<HouseAdd />}></Route>
         <Route path="/house/edit/:id" element={<HouseEdit />}></Route>
         <Route path="/contact/" element={<Contact/>} ></Route>
        </Routes> 
        
      </div>
    </BrowserRouter>
  );
}

export default App;
