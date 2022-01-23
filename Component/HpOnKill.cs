﻿using System.Collections;
using ThunderRoad;
using Wully.Utils;

namespace GameModeLoader.Component {
	public class HpOnKill : LevelModule {
		public int hpAmount = 10;

		public override IEnumerator OnLoadCoroutine() {
			if (Level.current.GetOptionAsBool("hponkill", true)) {
				EventManager.onCreatureKill += EventManager_onCreatureKill;
			}

			yield return base.OnLoadCoroutine();
		}

		private void EventManager_onCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance,
			EventTime eventTime) {
			if ( eventTime == EventTime.OnStart || player || !collisionInstance.sourceColliderGroup )
				return;
			if ( collisionInstance.sourceColliderGroup.collisionHandler.item?.lastHandler?.creature.player ) {
				player.creature.Heal(hpAmount, player.creature);
			} else {
				if ( !collisionInstance.sourceColliderGroup.collisionHandler.ragdollPart?.ragdoll.creature.player )
					return;
				player.creature.Heal(hpAmount, player.creature);
			}
			
		}

		public override void OnUnload() {
			if (Level.current.GetOptionAsBool("hponkill", true)) {
				EventManager.onCreatureKill -= EventManager_onCreatureKill;
			}

			base.OnUnload();
		}
	}
}