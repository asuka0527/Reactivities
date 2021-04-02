import React, { useEffect, useState } from "react";

import { Activity } from "../models/activity";
import Navbar from "./Navbar";

import AcitivityDashboard from "../../features/activities/dashboard/AcitivityDashboard";
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { Container } from "semantic-ui-react";

import { observer } from "mobx-react-lite";

function App() {
  const { activityStore } = useStore();
  // How to use created interfaces as a <Type Parameter>
  const [activities, setActivities] = useState<Activity[]>([]);

  const [submitting, setSubmmitting] = useState(false);

  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore]);

  if (activityStore.loadingInitial)
    return <LoadingComponent inverted content="Loading app..." />;
  return (
    <>
      <Navbar />
      <Container style={{ marginTop: "7em" }}>
        <AcitivityDashboard />
      </Container>
    </>
  );
}

export default observer(App);
