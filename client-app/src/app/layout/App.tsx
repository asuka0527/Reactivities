import Navbar from "./Navbar";
import AcitivityDashboard from "../../features/activities/dashboard/AcitivityDashboard";
import { Container } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { Route, useLocation } from "react-router";
import HomePage from "../../features/home/HomePage";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";

function App() {
  // for create and Edit form - bec these both use the same component we have to past the key to let react know that something is changing
  const location = useLocation();

  return (
    <>
      <Route path="/" component={HomePage} exact />
      <Route
        path={"/(.+)"}
        render={() => (
          <>
            <Navbar />
            <Container style={{ marginTop: "7em" }}>
              <Route path="/activities" component={AcitivityDashboard} exact />
              <Route path="/activities/:id" component={ActivityDetails} />
              <Route
                key={location.key}
                path={["/createActivity", "/manage/:id"]}
                component={ActivityForm}
              />
            </Container>
          </>
        )}
      />
    </>
  );
}

export default observer(App);
