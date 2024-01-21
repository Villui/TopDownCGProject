using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour {

    private ParticleSystem ps;
    private List<ParticleSystem.Particle> triggerParticles = new();


    private void Awake() {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.AddCollider(Player.Instance.transform);
    }

    private void OnParticleTrigger() {
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, triggerParticles);
        PlayerCoinManager.Instance.AddCoins(1);

        for (int i = 0; i < triggerParticles.Count; i++) {
            ParticleSystem.Particle p = triggerParticles[i];
            p.remainingLifetime = 0f;
            triggerParticles[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, triggerParticles);
    }
}
