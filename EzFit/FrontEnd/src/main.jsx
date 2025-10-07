import React, { Suspense } from "react";
import { Provider } from "react-redux";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import { store } from "./store/Store";
import Spinner from "./views/spinner/Spinner";
import "./utils/i18n";
import { AuthProvider } from '/src/guards/firebase/firebaseContext';

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Provider store={store}>
      <Suspense fallback={<Spinner />}>
        <BrowserRouter>
          <AuthProvider>
            <App />
          </AuthProvider>
        </BrowserRouter>
      </Suspense>
    </Provider>
  </React.StrictMode>
);